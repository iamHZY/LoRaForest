<script setup lang="ts">
import { ref, onMounted } from "vue";
import { invoke } from "@tauri-apps/api/core";
import { listen } from "@tauri-apps/api/event";

let isConnect = ref(false);
let receivedData = ref("");
let humiVal = ref("-");
let tempVal = ref("-");
let presVal = ref("-");
let rainVal = ref("-");
let lightVal = ref("-");

onMounted(async ()=>{
    await listen<string>("Humi", (event) =>{
        humiVal.value = event.payload;
    });
    await listen<string>("Temp", (event) =>{
        tempVal.value = event.payload;
    });
    await listen<string>("Light", (event) =>{
        lightVal.value = event.payload;
    });
    await listen<string>("Rain", (event) =>{
        rainVal.value = event.payload;
    });
    await listen<string>("Pres", (event) =>{
        presVal.value = event.payload;
    });
    await listen<string>("serial-data", (event) =>{
        receivedData.value = event.payload + receivedData.value;
    });
});

async function getAvailabelPorts() {
    let list:string[] = await invoke("getAvailabelPorts");
    let portlist = document.getElementById("port") as HTMLSelectElement;
    portlist.innerHTML = "";
    for (let i = 0; i < list.length; i++){
        let option = document.createElement("Option");
        option.nodeValue = list[i];
        option.textContent = list[i];
        portlist.appendChild(option);
    }
}

async function openSeialPort() {
    let portlist = document.getElementById("port") as HTMLSelectElement;
    let port = portlist.options[portlist.selectedIndex].text;
    if (!isConnect.value){
        if (await invoke("start_reading", {port: port})){
        //alert(port + "打开成功");
            isConnect.value = true;
            let connectStatus = document.getElementById("connStatus") as HTMLSpanElement;
            connectStatus.textContent = "已连接"
        }else{
            alert(port + "打开失败");
        }
    }
}

async function init() {
    getAvailabelPorts();
}

init();

</script>

<template>
<body>
<div class="app-fullscreen">
    <!-- 环境参数监测区域: 没有groupbox，没有容器边框 -->
    <div class="sensors-area">
        <div class="param-header">环境参数实时监测</div>
        
        <!-- 传感器网格 6项: 光照/降雨/气温/气压/风速/湿度 -->
        <div class="sensor-grid">
            <div class="sensor-card">
                <div class="sensor-label">💡 光照强度 (Lux)</div>
                <input type="text" v-model="lightVal" class="sensor-value" id="lightVal" readonly placeholder="—">
            </div>
            <div class="sensor-card">
                <div class="sensor-label">🌧️ 降雨量 (mm/h)</div>
                <input type="text" v-model="rainVal" class="sensor-value" id="rainVal" readonly placeholder="—">
            </div>
            <div class="sensor-card">
                <div class="sensor-label">🌡️ 气温 (°C)</div>
                <input type="text" v-model="tempVal" class="sensor-value" id="tempVal" readonly placeholder="—">
            </div>
            <div class="sensor-card">
                <div class="sensor-label">⏲️ 气压 (hPa)</div>
                <input type="text" v-model="presVal" class="sensor-value" id="pressureVal" readonly placeholder="—">
            </div>
            <div class="sensor-card">
                <div class="sensor-label">💧 空气湿度 (%)</div>
                <input type="text" v-model="humiVal" class="sensor-value" id="humidityVal" readonly placeholder="—">
            </div>
        </div>

        <div>
            <lable for="port">串口</lable>
            <select id="port"></select>
        </div>
        
        <!-- 状态栏和连接按钮 (无背景框) -->
        <div class="control-bar">
            <div class="status-text">
                📡 状态: <span id="connStatus">未连接</span>
            </div>
            <button class="btn-connect" @click="openSeialPort" id="connectActionBtn">🔌 连接</button>
        </div>

        <!-- 原始数据调试区: 完全贴合屏幕底部且无边框分组容器 -->
        <div class="rawdata-area">
            <div class="raw-header">
                <div class="raw-title">📟 原始数据流 (调试用)</div>
            </div>
            <div class="log-container">
                <textarea v-model="receivedData" class="raw-log" id="rawLog" readonly wrap="on" placeholder="等待连接...&#10;点击「连接」模拟LoRa串口数据接收"></textarea>
            </div>
        </div>
    </div>
</div>
</body>

</template>

<style>
* {
            margin: 0;
            padding: 0;
            box-sizing: border-box;
            font-family: 'Segoe UI', 'Roboto', '微软雅黑', system-ui, -apple-system, sans-serif;
        }

        /* 完全贴合屏幕，无任何边距/圆角/阴影/容器边框 */
        body {
            background: #f4f7fc;
            height: 100vh;
            width: 100vw;
            overflow: hidden;
            position: fixed;
            top: 0;
            left: 0;
        }

        /* 主应用直接占满全屏，无额外padding/margin */
        .app-fullscreen {
            display: flex;
            flex-direction: column;
            height: 100%;
            width: 100%;
            background: #ffffff;
        }

        /* 上部传感器区域 — 无边框无分组卡片，极简 */
        .sensors-area {
            flex: 0 0 auto;
            background: #ffffff;
            padding: 20px 24px 16px 24px;
            border-bottom: 1px solid #e2edf2;
        }

        /* 参数标题行 (轻微区分，但无容器) */
        .param-header {
            font-size: 1rem;
            font-weight: 600;
            color: #1e4a6b;
            margin-bottom: 18px;
            letter-spacing: 0.3px;
            display: flex;
            align-items: center;
            gap: 8px;
        }
        .param-header:before {
            content: "🌲";
            font-size: 1.2rem;
        }

        /* 6列网格布局，完全贴合无装饰 */
        .sensor-grid {
            display: grid;
            grid-template-columns: repeat(6, 1fr);
            gap: 18px 20px;
            margin-bottom: 20px;
        }

        .sensor-card {
            display: flex;
            width: 17vw;
            min-width: 120px;
            flex-direction: column;
            gap: 8px;
        }

        .sensor-label {
            font-size: 0.8rem;
            font-weight: 500;
            color: #2c5a74;
            text-align: center;
            letter-spacing: 0.3px;
            background: transparent;
        }

        .sensor-value {
            background: #f0f6fa;
            border: 1px solid #cde3ec;
            border-radius: 14px;
            padding: 10px 8px;
            text-align: center;
            font-size: 1rem;
            font-weight: 600;
            color: #0f2c3b;
            font-family: 'JetBrains Mono', 'Cascadia Code', monospace;
            transition: 0.1s;
            box-shadow: none;
            outline: none;
        }

        .sensor-value:read-only {
            cursor: default;
            background-color: #ffffff;
        }

        /* 状态栏 + 连接按钮 — 完全平贴，无圆角背景盒子 */
        .control-bar {
            display: flex;
            align-items: center;
            justify-content: space-between;
            flex-wrap: wrap;
            gap: 12px;
            margin-top: 4px;
            padding-top: 4px;
        }

        .status-text {
            font-size: 0.85rem;
            font-weight: 500;
            color: #1f5068;
            background: transparent;
            padding: 0;
        }

        .status-text span {
            font-weight: 700;
            color: #000000;
        }

        .btn-connect {
            background: #2c6e9e;
            border: none;
            font-weight: 600;
            font-size: 0.85rem;
            padding: 8px 28px;
            border-radius: 30px;
            color: white;
            cursor: pointer;
            transition: 0.2s;
            letter-spacing: 0.5px;
        }

        .btn-connect:hover {
            background: #1a4f73;
        }

        .clear-log-btn {
            background: transparent;
            border: none;
            color: #7e9ab5;
            font-size: 0.7rem;
            cursor: pointer;
            padding: 4px 12px;
            border-radius: 20px;
            transition: 0.1s;
        }

        .clear-log-btn:hover {
            background: #1e2a3a;
            color: #e2e8f0;
        }

        /* 完全填满剩余空间，无边框滚动条 */
        .log-container {
            flex: 1;
            background: #0b111e;
            overflow: auto;
            width: 100%;
            height: 100%;
            /* 确保内部textarea完全填充无留白 */
        }
        /* 原始数据区域 — 占据剩余所有空间，无边框无圆角，完全贴合 */
        .rawdata-area {
            flex: 1;
            display: flex;
            flex-direction: column;
            background: #0f1724;
            width: 100%;
            height: 100%;
            /* 无任何margin/padding让textarea紧贴边缘? 保留少许内边距提升可读性，但无容器边框 */
            padding: 12px 16px 16px 16px;
            overflow: hidden;
        }

        .raw-header {
            display: flex;
            justify-content: space-between;
            align-items: baseline;
            margin-bottom: 10px;
            flex-shrink: 0;
        }

        .raw-title {
            font-size: 0.8rem;
            font-weight: 500;
            color: #9ab3c5;
            letter-spacing: 0.5px;
            background: transparent;
        }

        .raw-log {
            width: 100%;
            height: 100%;
            background: #0b111e;
            border: none;
            color: #cbdde6;
            font-family: 'Fira Code', 'Cascadia Code', monospace;
            font-size: 0.75rem;
            line-height: 1.45;
            resize: none;
            outline: none;
            padding: 12px 8px;
            white-space: pre-wrap;
            word-break: break-all;
            overflow-y: auto;
        }

        /* 响应式: 平板屏幕将网格转为3列 */
        @media (max-width: 860px) {
            .sensor-grid {
                grid-template-columns: repeat(3, 1fr);
                gap: 16px;
            }
            .sensors-area {
                padding: 16px 20px 12px 20px;
            }
        }

        .raw-log:read-only {
            cursor: default;
        }

        @media (max-width: 560px) {
            .sensor-grid {
                grid-template-columns: repeat(2, 1fr);
            }
            .control-bar {
                flex-direction: column;
                align-items: stretch;
            }
            .btn-connect {
                text-align: center;
            }
        }

        /* 去掉所有容器阴影、边框线条、圆角背景组，完全贴合屏幕 */
        ::-webkit-scrollbar {
            width: 5px;
            height: 5px;
        }
        ::-webkit-scrollbar-track {
            background: #1f2a36;
        }
        ::-webkit-scrollbar-thumb {
            background: #4c6a82;
            border-radius: 6px;
        }
</style>