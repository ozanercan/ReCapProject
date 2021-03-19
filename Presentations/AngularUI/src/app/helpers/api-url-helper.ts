import { Dictionary } from '../models/dictionary';
import { environment } from './../../environments/environment';

export class ApiUrlHelper {
  static getUrl(path: string): string {
    return environment.apiUrl + environment.apiPrefix + path;
  }

  static getUrlWithParameters(path: string, parameters: Dictionary[]): string {
    let baseUrl = this.getUrl(path) + '?';

    // ?n=John&n=Susan
    for (let i = 0; i < parameters.length; i++) {
      const parameter = parameters[i];

      baseUrl += parameter.key;

      baseUrl += '=';

      baseUrl += parameter.value;

      if (i < parameters.length) {
        baseUrl += '&';
      }
    }
    return baseUrl;
  }
}
