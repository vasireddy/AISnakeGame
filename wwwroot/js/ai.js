import { pipeline, env } from 'https://cdn.jsdelivr.net/npm/@xenova/transformers@2.17.2';

// Configuration for local inference
env.allowLocalModels = false;
env.useBrowserCache = true;

let generator = null;
let modelName = 'Xenova/SmolLM2-360M-Instruct';

window.aiBridge = {
    init: async function() {
        if (generator) return;
        generator = await pipeline('text-generation', modelName, {
            device: navigator.gpu ? 'webgpu' : 'wasm'
        });
    },

    getBackendInfo: function() {
        return navigator.gpu ? "WebGPU" : "WASM (CPU Fallback)";
    },

    generateConfig: async function(prompt) {
        await this.init();

        const systemPrompt = `You are a game designer for a Snake game.
Generate a JSON object containing both "theme" and "rules" based on the user's prompt.

Theme fields: name, snakeColor, snakeHeadColor, foodColor, backgroundColor, gridColor, textColor, fontFamily.
Rules fields: walls (bool), teleport (bool), speedMultiplier (float).

The JSON must follow this exact format:
{
  "theme": {
    "name": "...",
    "snakeColor": "...",
    ...
  },
  "rules": {
    "walls": true,
    "teleport": false,
    "speedMultiplier": 1.0
  }
}
Only output the JSON object. No other text.`;

        const fullPrompt = `<|im_start|>system\n${systemPrompt}<|im_end|>\n<|im_start|>user\n${prompt}<|im_end|>\n<|im_start|>assistant\n`;

        const output = await generator(fullPrompt, {
            max_new_tokens: 250,
            temperature: 0.7,
            do_sample: true,
            return_full_text: false
        });

        let text = output[0].generated_text;

        try {
            const start = text.indexOf('{');
            const end = text.lastIndexOf('}') + 1;
            if (start !== -1 && end !== -1) {
                text = text.substring(start, end);
            }
            JSON.parse(text);
            return text;
        } catch (e) {
            console.error("AI JSON Parse Error:", text);
            return null;
        }
    }
};
