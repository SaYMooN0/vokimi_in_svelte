export enum GeneralTestAnswerType {
    TextOnly = "text_only",
    TextAndImage = "text_and_image",
    ImageOnly = "image_only"
}

export namespace GeneralTestAnswerTypeUtils {
    export function fromId(id: string): GeneralTestAnswerType {
        for (const key in GeneralTestAnswerType) {
            if (GeneralTestAnswerType[key as keyof typeof GeneralTestAnswerType] === id) {
                return GeneralTestAnswerType[key as keyof typeof GeneralTestAnswerType];
            }
        }
        return GeneralTestAnswerType.TextOnly;
    }

    export function getId(type: GeneralTestAnswerType): string {
        return type;
    }

    export function getFullName(type: GeneralTestAnswerType): string {
        switch (type) {
            case GeneralTestAnswerType.TextOnly:
                return "Text Only";
            case GeneralTestAnswerType.TextAndImage:
                return "Text and Image";
            case GeneralTestAnswerType.ImageOnly:
                return "Image Only";
            default:
                return "Unknown Answer Type";
        }
    }
}
