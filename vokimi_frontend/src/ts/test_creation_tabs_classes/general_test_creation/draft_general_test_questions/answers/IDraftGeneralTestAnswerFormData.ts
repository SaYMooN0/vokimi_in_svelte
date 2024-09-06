export interface IDraftGeneralTestAnswerFormData {
    relatedResults: { [key: string]: string };
    checkForErr(): string | null;
}