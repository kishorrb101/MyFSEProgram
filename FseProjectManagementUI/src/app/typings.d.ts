// https://hackernoon.com/import-json-into-typescript-8d465beded79
// https://github.com/chybie/ts-json/blob/master/typings.d.ts

declare module "*.json" {
  const value: any;
  export default value;
}
