<script lang="ts">
    import { LanguageUtils } from "../../../../ts/enums/Language";
    import { TestTemplateUtils } from "../../../../ts/enums/TestTemplate";
    import { StringUtils } from "../../../../ts/utils/StringUtils";
    import type { ViewTestBaseInfoTabData } from "../../../../ts/page_classes/view_test_page_classes/middle_section_tabs_classes/ViewTestBaseInfoTabData";

    export let tabData: ViewTestBaseInfoTabData;
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
    <div class="tags-container">
        {#each tabData.tags as tag}
            <div class="tag-display">{tag}</div>
        {/each}
    </div>
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

    .tags-container {
        margin: 10px;
        padding: 8px;
        border-radius: 4px;
        background-color: var(--back);
    }

    .tag-display {
    }
</style>
