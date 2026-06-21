# GitHub Pages Deployment

AI Snake Studio is a **Static Site**. This means it can be hosted on any web server without needing a backend (no ASP.NET server, no Node.js, no Database).

## Why it works on GitHub Pages
1. **WASM:** The .NET code is compiled to WebAssembly, which the browser executes.
2. **Local AI:** Transformers.js runs entirely in the browser using the user's GPU/CPU.
3. **No Backend:** All data (themes, rules) is generated and stored locally in the session.

## Deployment Flow
When you push to the `main` branch, a GitHub Action is triggered:

1. **`dotnet publish`:** This command compiles the C# code and collects all assets (HTML, CSS, JS, WASM) into a `wwwroot` folder.
2. **`.nojekyll`:** We add this file to tell GitHub Pages NOT to process the files with Jekyll (which can sometimes break files starting with underscores, like `_framework/`).
3. **Artifacts:** The contents of `wwwroot` are pushed to the `gh-pages` branch.
4. **Hosting:** GitHub serves these static files to the browser.

## Published Files
* `_framework/`: Contains the .NET WASM runtime and application DLLs.
* `js/`: Contains our AI and Rendering logic.
* `index.html`: The entry point.
* `css/`: Stylesheets.

## Deployment Checklist
- [ ] Ensure `<base href="/" />` in `index.html` matches your hosting path.
- [ ] Ensure the GitHub Actions secret/permission allows pushing to the `gh-pages` branch.
- [ ] Verify that `dotnet-version: '10.0.x'` is used in the workflow.
