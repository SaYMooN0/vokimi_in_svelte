<script lang="ts">
    import { LanguageUtils } from "../../../../ts/enums/Language";
    import { TestTemplateUtils } from "../../../../ts/enums/TestTemplate";
    import { StringUtils } from "../../../../ts/utils/StringUtils";
    import type { ViewTestBaseInfoTabData } from "../../../../ts/page_classes/view_test_page_classes/middle_section_tabs_classes/ViewTestBaseInfoTabData";
    import TagsViewComponent from "./info_tab_components/TagsViewComponent.svelte";

    export let tabData: ViewTestBaseInfoTabData;
    export let testId: string;
</script>

<div class="test-info-tab">
    <div class="type-and-lang-container">
        <div class="test-type">
            Template: {TestTemplateUtils.getFullName(tabData.template)}
        </div>
        <div class="test-lang">
            Language: {LanguageUtils.getFullName(tabData.language)}
        </div>
    </div>
    {#if !StringUtils.isNullOrWhiteSpace(tabData.testDescription)}
        <div class="test-description">Description</div>
    {/if}
    <TagsViewComponent
        {testId}
        tags={tabData.tags}
        tagsSuggestionsAllowed={tabData.tagsSuggestionsAllowed}
    />
</div>

<style>
    .type-and-lang-container {
        display: grid;
        grid-template-columns: 1fr 1fr;
    }

    .type-and-lang-container div {
        display: flex;
        justify-content: center;
        align-items: center;
    }

   
</style>
