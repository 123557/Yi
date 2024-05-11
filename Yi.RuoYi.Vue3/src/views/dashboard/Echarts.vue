<template>
    <div ref="eChaDom" :style="{ height: h }" />
</template>

<script setup>
import { watch, onMounted, onBeforeUnmount, ref, shallowRef } from "vue";
import * as echarts from "echarts";
import debounce from 'lodash/debounce'


const props = defineProps({
    h: {
        type: String,
        default: '360px'
    },
    options: {
        type: Object,
        default: () => ({})
    },
    theme: {
        type: String,
        // default: 'dark'
    }
})
// const eChaDom = ref(null); // 这样resize有报错
// const chart = ref(null)
const eChaDom = shallowRef(null); // 这样resize就没报错了
const chart = shallowRef(null)
const init = () => {
    chart.value = echarts.init(eChaDom.value, props.theme)
    chart.value.setOption(props.options);
    window.addEventListener('resize', debounce(resizeFn, 360))
}
const resizeFn = () => {
    chart.value.resize()
}

onMounted(() => {
    init()
})

watch(
    () => props.options,
    (newOptions) => {
        chart.value.setOption(newOptions);
    },
    { deep: true }
)

onBeforeUnmount(() => {
    window.removeEventListener('resize', resizeFn)
})
</script>