<script lang="ts">
    import { PublishedTestBriefInfo } from "../../../../ts/my_tests_page/PublishedTestBriefInfo";
    export let publishedTests: PublishedTestBriefInfo[] = [];

    if (publishedTests.length === 0) {
        //load from api
    }
</script>

<p>Published tab</p>
{#each publishedTests as test}
    <p>{test.name}</p>
{/each}
