<script lang="ts">
    import type { MyTestsPageTestViewModel } from "./../../../ts/MyTestsPageTestViewModel";
    export let draftTests: MyTestsPageTestViewModel[] = [];
</script>

<p>DraftTests tab</p>
{#each draftTests as test}
    <p>{test.name}</p>
{/each}
