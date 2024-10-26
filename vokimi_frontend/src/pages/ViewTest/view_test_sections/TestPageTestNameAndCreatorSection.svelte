<script lang="ts">
    import type { TestNameAndCreatorSectionClass } from "../../../ts/page_classes/view_test_page_classes/TestNameAndCreatorSectionClass";

    export let sectionData: TestNameAndCreatorSectionClass;
</script>

<div class="test-name">{sectionData.testName}</div>
<p class="creator-link">
    by <a href="/user/{sectionData.creatorId}">{sectionData.creatorUsername}</a>
</p>

<style>
    .test-name {
        font-size: 24px;
    }

    .creator-link {
        margin: 4px 0 4px 10px;
        font-size: 19px;
        color: var(--text-faded);
    }

    .creator-link a {
        color: var(--primary);
        text-decoration: none;
        padding: 2px 6px;
        border-radius: 4px;
    }

    .creator-link a:hover {
        color: var(--primary-hov);
    }
    .creator-link a:active {
        background-color: var(--back-secondary);
    }
</style>
