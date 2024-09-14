export class GeneralTestCreationResultsTabData {
    readonly results: DraftGeneralTestResultDataToEdit[] = [];

    constructor(
        results: DraftGeneralTestResultDataToEdit[]
    ) {
        this.results = results;
    }
    static empty(): GeneralTestCreationResultsTabData {
        return new GeneralTestCreationResultsTabData([]);
    }
    isEmpty(): boolean {
        return this.results.length === 0;
    }
}
export class DraftGeneralTestResultDataToEdit {
    readonly id: string;
    readonly name: string;
    readonly text: string;
    readonly imagePath: string | null;

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