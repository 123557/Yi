import { login, logout, getInfo } from '@/api/login'
import { getToken, setToken, removeToken,getTenantId,setTenantId,removeTenantId } from '@/utils/auth'
import defAva from '@/assets/images/profile.jpg'
import {getUserListData} from '@/api/question/question'
const useUserStore = defineStore(
  'user',
  {
    state: () => ({
      token: getToken(),
      id:'',
      name: '',
      username:'',
      avatar: '',
      roles: [],
      permissions: [],
      userlist:[],
      tenantId:getTenantId()
    }),
    actions: {
      // 登录
      login(userInfo,tenantId) {
        const username = userInfo.username.trim()
        const password = userInfo.password
        const code = userInfo.code
        const uuid = userInfo.uuid

        //登录之前先设置租户id，用户登录接口的请求设置
        setTenantId(tenantId);
        this.tenantId=tenantId;
        return new Promise((resolve, reject) => {
          login(username, password, code, uuid).then(res => {
            setToken(res.data.token);
            this.token = res.data.token;
            resolve();
          }).catch(error => {
            removeTenantId();
            this.tenantId=null;
            reject(error)
          })
        })
      },
      // 获取用户信息
      getInfo() {

        return new Promise((resolve, reject) => {
          getInfo().then(response => {
            const res=response.data;
            const user = res.user
            const avatar = (user.icon == "" || user.icon == null) ? defAva : import.meta.env.VITE_APP_BASE_API + "/file/"+user.icon;
    
            if (res.roleCodes && res.roleCodes.length > 0) { // 验证返回的roles是否是一个非空数组
              this.roles = res.roleCodes
              this.permissions = res.permissionCodes
              // this.roles = ["admin"];
              // this.permissions=["*:*:*"]

            } else {
              this.roles = ['ROLE_DEFAULT']
            }
            getUserListData().then((res)=>{
              res.data.map(p => ({ label: p.name, value: p.name}))
              this.userlist = res.data;
            })
            // this.roles = ["admin"];
            // this.permissions=["*:*:*"]
            this.id = user.id
            this.name = user.userName
            this.username =user.nick
            this.avatar = avatar;
            resolve(res)
          }).catch(error => {
            reject(error)
          })



        })
      },
      // 退出系统
      logOut() {
        return new Promise((resolve, reject) => {
          logout(this.token).then(() => {
            this.token = ''
            this.roles = []
            this.permissions = []
            removeToken()
            removeTenantId()
            resolve()
          }).catch(error => {
            reject(error)
          })
        })
      }
    }
  })

export default useUserStore
