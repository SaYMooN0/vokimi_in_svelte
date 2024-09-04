export enum Language {
    Eng = "eng",
    Rus = "rus",
    Spa = "spa",
    Ger = "ger",
    Fra = "fra",
    Other = "other"
}

export namespace LanguageUtils {
    export function fromId(id: string): Language {
        for (const key in Language) {
            if (Language[key as keyof typeof Language] === id) {
                return Language[key as keyof typeof Language];
            }
        }
        return Language.Other;
    }
    export function getFullName(language: Language): string {
        switch (language) {
            case Language.Eng:
                return "English";
            case Language.Rus:
                return "Русский";
            case Language.Spa:
                return "Español";
            case Language.Ger:
                return "Deutsch";
            case Language.Fra:
                return "Français";
            case Language.Other:
                return "Other";
            default:
                return "Unknown Language";
        }
    }
}