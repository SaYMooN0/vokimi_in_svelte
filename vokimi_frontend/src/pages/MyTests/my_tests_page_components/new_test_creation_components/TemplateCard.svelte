<script lang="ts">
    import {
        TestTemplate,
        getTemplateFeatures,
    } from "../../../../ts/enums/TestTemplate";
    import GeneralTemplateIcon from "../../../../components/icons/test_templates_icons/GeneralTemplateIcon.svelte";
    import ScoringTemplateIcon from "../../../../components/icons/test_templates_icons/ScoringTemplateIcon.svelte";
    import CorrectAnswersTemplateIcon from "../../../../components/icons/test_templates_icons/CorrectAnswersTemplateIcon.svelte";

    export let template: TestTemplate;
    export let isActive = false;
    export let onClick;
    function getTemplateIcon(template: TestTemplate) {
        switch (template) {
            case TestTemplate.General:
                return GeneralTemplateIcon;
            case TestTemplate.Scoring:
                return ScoringTemplateIcon;
            case TestTemplate.CorrectAnswers:
                return CorrectAnswersTemplateIcon;
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
        height: 240px;
        aspect-ratio: 1.8/1;
        padding: 12px 20px;
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
    @media screen and (max-width: 1600px) {
        .template-card {
            height: 220px;
            aspect-ratio: 1.8/1;
            grid-template-rows: 60px 1fr;
        }
    }
</style>
