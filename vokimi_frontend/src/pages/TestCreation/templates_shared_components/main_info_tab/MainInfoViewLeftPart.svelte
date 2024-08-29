<script lang="ts">
    import {
        TestTemplate,
        TestTemplateUtils,
    } from "../../../../ts/enums/TestTemplate";
    import { Language, LanguageUtils } from "../../../../ts/enums/Language";
    import {
        TestPrivacy,
        TestPrivacyUtils,
    } from "../../../../ts/enums/TestPrivacy";

    export let template: TestTemplate;
    export let testName: string;
    export let description: string;
    export let language: Language;
    export let privacy: TestPrivacy;
</script>

<p class="test-template">
    <span class="property-label">Test template: </span>
    <span class="property-val">
        {TestTemplateUtils.getFullName(template)}
    </span>
</p>
<p class="test-name">
    <span class="property-label">Test name: </span>
    {testName}
</p>
<p class="test-description">
    <span class="property-label">Test description: </span>
    {#if description === null || description === ""}
        (No description)
    {:else}
        {description}
    {/if}
</p>
<div class="lang-and-priv-div">
    <div class="lang-div">
        <span class="property-label">Language: </span>
        {LanguageUtils.getFullName(language)}
    </div>
    <div class="priv-div">
        <span class="property-label">Privacy: </span>
        {TestPrivacyUtils.getFullName(privacy)}
    </div>
</div>

<style>
    .property-label {
        font-weight: 500;
        font-size: 17px;
    }
    .property-val {
        font-size: 22px;
        font-weight: 400;
    }
    .lang-and-priv-div {
        display: grid;
        grid-template-columns: 1fr 1fr;
        width: 100%;
        justify-content: center;
        align-items: center;
    }
    .lang-and-priv-div div {
        justify-self: center;
        width: fit-content;
    }
</style>
