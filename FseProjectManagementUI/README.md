
# Project Manager

Install node.js
Install npm

Run `npm uninstall -g @angular/cli`
Run `npm npm cache clean`
Run `npm install -g @angular/cli@latest`

Run `npm install`


## Debugger for Chrome
https://github.com/Microsoft/vscode-recipes/tree/master/Angular-CLI
Visual Studio Code -> TERMINAL TAB
1. In (1:node) run [npm start]
2. Go [Debug] / (Ctrl+Shift+D)
3. Run {ng serve}

## Build errors
Node Sass could not find a binding for your current environment: Windows 64-bit with Node.js 10.x
 - https://stackoverflow.
com/questions/37986800/node-sass-could-not-find-a-binding-for-your-current-environment

## Deploy an Angular App From Visual Studio Code to Azure
https://dzone.com/articles/deploy-an-angular-app-from-visual-studio-code-to-a-1
1. On Azure App settings, set: WEBSITE_NODE_DEFAULT_VERSION = 10.6.0

## Deploy to IIS
1. Install url-rewrite. https://www.iis.net/downloads/microsoft/url-rewrite

## Build
https://stackoverflow.com/questions/51258031/angular-cli-how-to-use-ng-build-prod-command-for-with-environment-staging-t


## UX upgrade
1. https://wrapbootstrap.com/theme/angle-bootstrap-admin-template-WB04HF123
2. ng new my-sassy-app --style=scss
3. npm install @progress/kendo-angular-buttons @progress/kendo-angular-dateinputs @progress/kendo-angular-dialog @progress/kendo-angular-dropdowns @progress/kendo-angular-excel-export @progress/kendo-angular-grid @progress/kendo-angular-inputs @progress/kendo-angular-intl @progress/kendo-angular-l10n @progress/kendo-angular-label @progress/kendo-angular-tooltip @progress/kendo-angular-upload @progress/kendo-data-query @progress/kendo-drawing @progress/kendo-theme-default angular-2-local-storage bootstrap country-code-lookup country-state-city primeicons primeng --save

## Development server

Run `ng serve` for a dev server. Navigate to `http://localhost:4200/`. The app will automatically reload if you change any of the source files.

## Code scaffolding

Run `ng generate component component-name` to generate a new component. You can also use `ng generate directive|pipe|service|class|guard|interface|enum|module`.

## Build

Run `ng build` to build the project. The build artifacts will be stored in the `dist/` directory. Use the `--prod` flag for a production build.

