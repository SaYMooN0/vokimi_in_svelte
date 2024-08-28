export enum TestTemplate {
    General = "General",
    Scoring = "Scoring",
    CorrectAnswers = "CorrectAnswers",
}

export namespace TestTemplate {
    const idToEnumMap: { [key: string]: TestTemplate } = {
        "general": TestTemplate.General,
        "scoring": TestTemplate.Scoring,
        "correct_answers": TestTemplate.CorrectAnswers,
    };

    export function fromId(id: string): TestTemplate {
        return idToEnumMap[id] || TestTemplate.General;
    }
}

export function getTemplateFeatures(template: TestTemplate): string[] {
    switch (template) {
        case TestTemplate.General:
            return [
                "Completely customize your test the way you want it",
                "No restrictions or necessities (almost)",
            ];
        case TestTemplate.Scoring:
            return [
                "Let the test takers see how well do they meet this or that criterion",
                "Specially selected types of questions and the method of evaluating answers",
            ];
        case TestTemplate.CorrectAnswers:
            return ["", ""];
        default:
            throw new Error("Template not implemented");
    }
}
