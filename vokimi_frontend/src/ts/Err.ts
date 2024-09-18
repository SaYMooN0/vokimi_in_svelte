export class Err {
    private readonly message: string;

    constructor(message: string);
    constructor(ex: Error);
    constructor(arg: string | Error) {
        if (typeof arg === 'string') {
            this.message = arg;
        } else {
            this.message = arg.message;
        }
    }

    toString(): string {
        return this.message;
    }

    static none(): Err {
        return new Err('');
    }

    notNone(): boolean {
        return this.message !== '';
    }
}
