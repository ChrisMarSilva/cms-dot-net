type GUID = string & { isGuid: true };

export class Categoria {
    public id: string; // GUID
    public nome: string;
    public imagemUrl: string;

    constructor(id: string, nome: string, imagemUrl: string) {
        this.id = id;
        this.nome = nome;
        this.imagemUrl = imagemUrl;
    };

    describe() {
        console.log(`${this.id} - ${this.nome} - ${this.imagemUrl}`);
    }
}