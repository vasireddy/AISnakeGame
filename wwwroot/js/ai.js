import { pipeline, env } from 'https://cdn.jsdelivr.net/npm/@xenova/transformers@2.17.2';

// Configuration for local inference
env.allowLocalModels = false;
env.useBrowserCache = true;

let generator = null;
let modelName = 'Xenova/SmolLM2-360M-Instruct';

// Use a self-initializing approach
async function getGenerator() {
    if (!generator) {
        console.log("Loading AI model...");
        generator = await pipeline('text-generation', modelName, {
            device: navigator.gpu ? 'webgpu' : 'wasm'
        });
        console.log("AI model loaded.");
    }
    return generator;
}

window.aiBridge = {
    getBackendInfo: function() {
        return navigator.gpu ? "WebGPU" : "WASM (CPU Fallback)";
    },

    generateConfig: async function(prompt) {
        const gen = await getGenerator();

        const systemPrompt = `You are a game designer for a Snake game.
Generate a JSON object containing both "theme" and "rules" based on the user's prompt.

Theme fields: name, snakeColor, snakeHeadColor, foodColor, backgroundColor, gridColor, textColor, fontFamily.
Rules fields: walls (bool), teleport (bool), speedMultiplier (float), ghostMode (bool).

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
    "speedMultiplier": 1.0,
    "ghostMode": false
  }
}
Only output the JSON object. No other text.`;

        const fullPrompt = `<|im_start|>system\n${systemPrompt}<|im_end|>\n<|im_start|>user\n${prompt}<|im_end|>\n<|im_start|>assistant\n`;

        const output = await gen(fullPrompt, {
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

console.log("AI Bridge initialized.");
