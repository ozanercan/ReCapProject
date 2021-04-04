export class AccessTokenDto {
  token!: string;
  expiration!: Date;
  claims!: string[];
}
