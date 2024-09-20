<script lang="ts">
    import { MyTestsPageTestViewModel } from "../../../../ts/MyTestsPageTestViewModel";
    export let draftTests: MyTestsPageTestViewModel[] = [];

    async function fetchDraftTests() {
        fetched = true;
    }
    let fetched: boolean = false;

    fetched = false;
    if (draftTests.length == 0) {
        fetched = true;
    } else {
        fetchDraftTests();
    }
</script>

{#if fetched}
    <h2 class="your-draft-tests-label">Your draft tests:</h2>
    {#each draftTests as test}
        <a href="/testCreation/{test.id}/main-info-view">
            <p>{test.name}</p>
        </a>
    {/each}
{:else}
    <div class="loading-div">
        <h2>Receiving information from server...</h2>
        <p>Please wait</p>
    </div>
{/if}

<style>
    .your-draft-tests-label {
        margin: 20px auto 0 auto;
        width: fit-content;
    }
</style>
