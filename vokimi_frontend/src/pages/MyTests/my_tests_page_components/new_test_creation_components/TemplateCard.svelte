<script lang="ts">
    import { TestTemplate } from "../../../../ts/enums/TestTemplate";
    import GeneralTemplateIcon from "../../../../components/icons/test_templates_icons/GeneralTemplateIcon.svelte";
    import KnowledgeTemplateIcon from "../../../../components/icons/test_templates_icons/KnowledgeTemplateIcon.svelte";
    export let template: TestTemplate;
    export let isActive = false;
    export let onClick;
    function getTemplateFeatures(template: TestTemplate): string[] {
        switch (template) {
            case TestTemplate.General:
                return [
                    "Completely customize your test the way you want it",
                    "No restrictions or necessities (almost)",
                ];
            case TestTemplate.Knowledge:
                return [
                    "See how well test takers know the subject",
                    "Specially selected types of questions and the method of evaluating answers",
                ];
            default:
                throw new Error("Template not implemented");
        }
    }
    function getTemplateIcon(template: TestTemplate) {
        switch (template) {
            case TestTemplate.General:
                return GeneralTemplateIcon;
            case TestTemplate.Knowledge:
                return KnowledgeTemplateIcon;
            default:
                throw new Error("Template not implemented");
        }
    }
</script>

<div
    class={isActive ? "template-card chosen-template" : "template-card"}
    on:click={onClick}
>
    <div class="icon-name-container">
        <svelte:component this={getTemplateIcon(template)} />

        <label class="name">
            {template}
        </label>
    </div>

    <div class="features">
        {#each getTemplateFeatures(template) as feature}
            <label>• {feature}</label>
        {/each}
    </div>
</div>

<style>
    .template-card {
        display: grid;
        grid-template-rows: 70px 1fr;
        height: 230px;
        aspect-ratio: 1.7/1;
        padding: 10px 20px;
        box-sizing: border-box;
        border-radius: 10px;
        border: 4px solid transparent;
        box-shadow: 0 4px 10px 0px #39393c26;
        font-family: "Roboto";
        transition: all 0.12s ease-in;
        cursor: pointer;
    }
    .template-card:hover {
        transform: scale(1.04);
        box-shadow: 0 3px 14px 2px #573aa026;
    }
    .chosen-template {
        border-radius: 12px;
        border-color: var(--primary);
        box-shadow: 0 3px 15px 3px #5c4db726;
        transform: scale(1.065) !important;
    }

    .icon-name-container {
        justify-self: center;
        width: 80%;
        padding: 3% 0;
        box-sizing: border-box;
        display: flex;
        flex-direction: row;
        align-items: center;
        gap: 10px;
    }

    .name {
        cursor: inherit;
        font-weight: 600;
        color: var(--primary);
        font-size: 24px;
        display: flex;
        align-items: center;
    }
    .icon-name-container > :global(svg) {
        height: 100%;
        aspect-ratio: 1/1;
        color: var(--primary);
    }
    .features {
        margin: 6px 0;
        border-radius: 10px;
        background-color: var(--back-secondary);
        display: flex;
        flex-direction: column;
        padding: 4px 8px;
        box-sizing: border-box;
    }

    .features label {
        cursor: inherit;
        margin-top: 6px;
        font-size: 18px;
        font-family: Roboto;
        color: var(--primary);
    }
</style>
