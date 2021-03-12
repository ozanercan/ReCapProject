import { INav } from './INav';

export interface IDropDownNav {
  title: string;
  route: string;
  childNavs: INav[];
}
