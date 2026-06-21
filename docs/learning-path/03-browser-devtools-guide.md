# Browser DevTools Guide

As a browser AI developer, the DevTools are your best friend.

## Network Tab
* **WASM Downloads:** Filter by "wasm" to see the .NET runtime loading.
* **Model Downloads:** Filter by "onnx" or "bin". You will see the Transformers.js model files downloading from the CDN.
* **Caching:** Notice that after the first load, the model files come from the "Disk Cache" or "Service Worker".

## Application Tab
* **Storage:** Transformers.js uses **IndexedDB** to store the AI models so they don't have to be re-downloaded every time.
* **Console:** Check for WebGPU errors here.

## Performance Tab
* **WebGPU usage:** Modern browsers show GPU usage in the performance monitor.

## Memory Tab
* **WASM Heap:** You can take a heap snapshot to see how much memory the .NET runtime and the AI model are consuming. (Warning: LLMs can use 500MB+ of RAM).
