import request from '@/utils/request'

export function get_question_statusData() {
    return request({
      url: `/question_status`,
      method: 'get'
    })
  }

  export function get_question_projectData() {
    return request({
      url: `/question_project`,
      method: 'get'
    })
  }

  export function get_question_priorityData() {
    return request({
      url: `/question_priority`,
      method: 'get'
    })
  }

  export function get_question_impactData() {
    return request({
      url: `/question_impact`,
      method: 'get'
    })
  }

  export function get_question_categoryData() {
    return request({
      url: `/question_category`,
      method: 'get'
    })
  }