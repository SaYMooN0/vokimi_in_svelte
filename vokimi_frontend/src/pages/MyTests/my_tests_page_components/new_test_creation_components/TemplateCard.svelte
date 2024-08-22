<script lang="ts">
    import { TestTemplate } from "../../../../ts/enums/TestTemplate";
import generalTemplateIcon from ".@/assets/general_template_icon.svg";
import knowledgeTemplateIcon from ".@/assets/knowledge_template_icon.svg";

    export let template:TestTemplate;
    export let isActive = false;
    export let onClick;

    function getTemplateFeatures(template:TestTemplate): string[] {
        switch (template) {
            case TestTemplate.General:
                return [
                    "Completely customize your test the way you want it",
                    "No restrictions or necessities (almost)"
                ];
            case TestTemplate.Knowledge:
                return [
                    "See how well test takers know the subject",
                    "Specially selected types of questions and the method of evaluating answers"
                ];
            default:
                throw new Error('Template not implemented');
    }
    function getTemplateIcon(template:TestTemplate): string {
        switch (template) {
            case TestTemplate.General:
                return generalTemplateIcon;
            case TestTemplate.Knowledge:
                return knowledgeTemplateIcon;
            default:
                throw new Error('Template not implemented');
        }
    }
    

</script>

<div class={isActive ? 'template chosen-template' : 'template'} on:click={onClick}>
    <div class="icon-name-container">
        <img
            src={getTemplateIcon(template)}
            class="vokimi-logo"
            alt="Vokimi Logo"
        />

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
</style>
