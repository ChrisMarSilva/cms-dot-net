export class Usuario {
    public email: string;
    public password: string;
    public token: string;

    constructor(email: string, password: string, token: string) {
        this.email = email;
        this.password = password;
        this.token = token;
    };

    describe() {
        console.log(`${this.email} - ${this.password} - ${this.token}`);
    }
}