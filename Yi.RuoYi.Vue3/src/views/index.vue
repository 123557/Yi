<template>
  <div class="app-container home">
    <el-row :gutter="20">
      <el-col :sm="24" :lg="12" style="padding-left: 20px">
        <h2>问题点管理系统</h2>
        <h3>当前在线人数：{{onlineNum}}</h3>
      </el-col>
    </el-row>
    <el-divider />

    <el-row :gutter="32">
      <el-col :xs="24" :sm="24" :lg="8">
        <eCha :options="data" h="300px" />
      </el-col>
      <el-col :xs="24" :sm="24" :lg="8">
        <eCha :options="data1" h="300px" />
      </el-col>
      <el-col :xs="24" :sm="24" :lg="8">
        <eCha :options="data2" h="300px" />
      </el-col>
      <el-col :xs="24" :sm="24" :lg="12">
        <eCha :options="data3" h="300px" />
      </el-col>
      <el-col :xs="24" :sm="24" :lg="12">
        <eCha :options="data4" h="300px" />
      </el-col>
    </el-row>
  </div>
</template>

<script setup name="Index">

import { ref } from "@vue/reactivity";

import { storeToRefs } from 'pinia';
import useSocketStore from '@/store/modules/socket';
import {
    get_question_statusData,get_question_projectData,
    get_question_priorityData,get_question_impactData,
    get_question_categoryData} from '@/api/echarts/echarts.js';

import eCha from "./dashboard/Echarts.vue";
// 定义一个ref用于存储异步获取的数据
const chart_question_status_Data = ref([]);
const chart_question_project_Data = ref([]);
const chart_question_priority_Data = ref([]);
const chart_question_impact_Data = ref([]);
const chart_question_category_Data = ref([]);


onMounted(async () => {
  try {
    const responseData = await get_question_statusData();
    const responseData1 = await get_question_projectData();
    const responseData2 = await get_question_priorityData();
    const responseData3 = await get_question_impactData();
    const responseData4 = await get_question_categoryData();
    chart_question_status_Data.value = responseData.data; 
    chart_question_project_Data.value = responseData1.data; 
    chart_question_priority_Data.value = responseData2.data; 
    chart_question_impact_Data.value = responseData3.data; 
    chart_question_category_Data.value = responseData4.data; 
    //console.log(chartData.value); // 确认数据已更新
  } catch (error) {
    console.error('Error fetching data:', error);
  }
});

// 使用chartData的值来动态设置data的值
const data = computed(() => ({
    title: {
    text: '问题点状态汇总',
    left: 'center'
  },
  tooltip: {
    trigger: 'item',
  },
  legend: {
    orient: 'vertical',
    left: 'left',
  },
  series: [
    {
      name: '汇总',
      type: 'pie',
      radius: '70%',
      data:  JSON.parse(JSON.stringify(chart_question_status_Data.value)) || [], // 如果chartData.value未定义或为空，使用空数组作为默认值
    }
  ],
  emphasis: {
        itemStyle: {
          shadowBlur: 10,
          shadowOffsetX: 0,
          shadowColor: 'rgba(0, 0, 0, 0.5)'
        }
      }
}));

const data1 = computed(() => ({
    title: {
    text: '问题点所属项目汇总',
    left: 'center'
  },
  tooltip: {
    trigger: 'item',
  },
  legend: {
    orient: 'vertical',
    left: 'left',
  },
  series: [
    {
      name: '汇总',
      type: 'pie',
      radius: '70%',
      data:  JSON.parse(JSON.stringify(chart_question_project_Data.value)) || [], 
    }
  ],
  emphasis: {
        itemStyle: {
          shadowBlur: 10,
          shadowOffsetX: 0,
          shadowColor: 'rgba(0, 0, 0, 0.5)'
        }
      }
}));

const data2 = computed(() => ({
    title: {
    text: '问题点影响度汇总',
    left: 'center'
  },
  tooltip: {
    trigger: 'item',
  },
  legend: {
    orient: 'vertical',
    left: 'left',
  },
  series: [
    {
      type: 'pie',
      name: '汇总',
      radius: '70%',
      data:  JSON.parse(JSON.stringify(chart_question_impact_Data.value)) || [], 
    }
  ],
  emphasis: {
        itemStyle: {
          shadowBlur: 10,
          shadowOffsetX: 0,
          shadowColor: 'rgba(0, 0, 0, 0.5)'
        }
      }
}));

const data3 = computed(() => ({
    title: {
    text: '问题点分类汇总',
    left: 'center'
  },
  tooltip: {
    trigger: 'item',
  },
  legend: {
    orient: 'vertical',
    left: 'left',
  },
  series: [
    {
        name: '汇总',
      type: 'pie',
      radius: '70%',
      data:  JSON.parse(JSON.stringify(chart_question_category_Data.value)) || [], 
    }
  ],
  emphasis: {
        itemStyle: {
          shadowBlur: 10,
          shadowOffsetX: 0,
          shadowColor: 'rgba(0, 0, 0, 0.5)'
        }
      }
}));

const data4 = computed(() => ({
    title: {
    text: '问题点优先级汇总',
    left: 'center'
  },
  tooltip: {
    trigger: 'item',
  },
  legend: {
    orient: 'vertical',
    left: 'left',
  },
  series: [
    {
        name: '汇总',
      type: 'pie',
      radius: '70%',
      data:  JSON.parse(JSON.stringify(chart_question_priority_Data.value)) || [], 
    }
  ],
  emphasis: {
        itemStyle: {
          shadowBlur: 10,
          shadowOffsetX: 0,
          shadowColor: 'rgba(0, 0, 0, 0.5)'
        }
      }
}));
const socketStore=useSocketStore();
const {onlineNum}=storeToRefs(socketStore);

</script>

<style scoped lang="scss">
.home {
  blockquote {
    padding: 10px 20px;
    margin: 0 0 20px;
    font-size: 17.5px;
    border-left: 5px solid #eee;
  }
  hr {
    margin-top: 20px;
    margin-bottom: 20px;
    border: 0;
    border-top: 1px solid #eee;
  }
  .col-item {
    margin-bottom: 20px;
  }

  ul {
    padding: 0;
    margin: 0;
  }

  font-family: "open sans", "Helvetica Neue", Helvetica, Arial, sans-serif;
  font-size: 13px;
  color: #676a6c;
  overflow-x: hidden;

  ul {
    list-style-type: none;
  }

  h4 {
    margin-top: 0px;
  }

  h2 {
    margin-top: 10px;
    font-size: 26px;
    font-weight: 100;
  }

  p {
    margin-top: 10px;

    b {
      font-weight: 700;
    }
  }

  .update-log {
    ol {
      display: block;
      list-style-type: decimal;
      margin-block-start: 1em;
      margin-block-end: 1em;
      margin-inline-start: 0;
      margin-inline-end: 0;
      padding-inline-start: 40px;
    }
  }
}
</style>

