
export class DraftGeneralTestResultEditingData {
    readonly id: string;
    name: string;
    text: string;
    imagePath: string | null;

    constructor(
        id: string,
        name: string,
        text: string,
        imagePath: string | null
    ) {
        this.id = id;
        this.name = name;
        this.text = text;
        this.imagePath = imagePath;
    }
}