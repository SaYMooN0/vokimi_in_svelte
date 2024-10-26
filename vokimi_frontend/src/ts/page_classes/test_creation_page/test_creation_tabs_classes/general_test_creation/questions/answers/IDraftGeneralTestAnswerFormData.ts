export interface IDraftGeneralTestAnswerFormData {
    relatedResults: { [key: string]: string };
    orderInQuestion: number;
    checkForErr(): string | null;
}