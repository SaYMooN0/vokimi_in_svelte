<script lang="ts">
    import type { MyTestsPageTestViewModel } from "../../../../ts/MyTestsPageTestViewModel";
    export let publishedTests: MyTestsPageTestViewModel[] = [];

    if (publishedTests.length === 0) {
        //load from api
    }
</script>

<p>Published tab</p>
{#each publishedTests as test}
    <p>{test.name}</p>
{/each}
