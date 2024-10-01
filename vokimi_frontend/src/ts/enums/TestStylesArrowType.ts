import ArrowDefault from "../../components/icons/test_styles_arrow_icons/ArrowDefault.svelte";
import ArrowDefaultCircle from "../../components/icons/test_styles_arrow_icons/ArrowDefaultCircle.svelte";
import ArrowDouble from "../../components/icons/test_styles_arrow_icons/ArrowDouble.svelte";
import ArrowDoubleSquare from "../../components/icons/test_styles_arrow_icons/ArrowDoubleSquare.svelte";
import ArrowLong from "../../components/icons/test_styles_arrow_icons/ArrowLong.svelte";

export enum TestStylesArrowType {
    Default = "default",
    DefaultCircle = "default_circle",
    Double = "double",
    DoubleSquare = "double_square",
    Long = "long",
}

export namespace TestStylesArrowTypeUtils {
    export function fromId(id: string): TestStylesArrowType {
        for (const key in TestStylesArrowType) {
            if (TestStylesArrowType[key as keyof typeof TestStylesArrowType] === id) {
                return TestStylesArrowType[key as keyof typeof TestStylesArrowType];
            }
        }
        return TestStylesArrowType.Default;
    }

    export function getFullName(arrow: TestStylesArrowType) {
        switch (arrow) {
            case TestStylesArrowType.Default:
                return "Default";
            case TestStylesArrowType.DefaultCircle:
                return "Default Circle";
            case TestStylesArrowType.Double:
                return "Double";
            case TestStylesArrowType.DoubleSquare:
                return "Double Square";
            case TestStylesArrowType.Long:
                return "Long";
            default:
                return "Unknown Arrow Type";
        }
    }
    export function getIcon(arrow: TestStylesArrowType) {
        switch (arrow) {
            case TestStylesArrowType.Default:
                return ArrowDefault;
            case TestStylesArrowType.DefaultCircle:
                return ArrowDefaultCircle;
            case TestStylesArrowType.Double:
                return ArrowDouble;
            case TestStylesArrowType.DoubleSquare:
                return ArrowDoubleSquare;
            case TestStylesArrowType.Long:
                return ArrowLong;
            default:
                throw new Error("Not implemented");
        }
    }
}


