export class TokenStoreService {

    private store: Storage = sessionStorage;

    public get token(): string {
        return this.store.getItem('catchemall-token');
    }

    public set token(value: string) {
        this.store.setItem('catchemall-token', value);
    }

}