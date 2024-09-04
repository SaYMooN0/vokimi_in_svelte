export enum TestTemplate {
    General = "general",
    Scoring = "scoring",
    CorrectAnswers = "correct_answers"
}

export namespace TestTemplateUtils {
    export function fromId(id: string): TestTemplate {
        for (const key in TestTemplate) {
            if (TestTemplate[key as keyof typeof TestTemplate] === id) {
                return TestTemplate[key as keyof typeof TestTemplate];
            }
        }
        return TestTemplate.General;
    }

    export function getFullName(template: TestTemplate): string {
        switch (template) {
            case TestTemplate.General:
                return "General Test";
            case TestTemplate.Scoring:
                return "Scoring Test";
            case TestTemplate.CorrectAnswers:
                return "Correct Answers Test";
            default:
                return "Unknown Test Template";
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

}


