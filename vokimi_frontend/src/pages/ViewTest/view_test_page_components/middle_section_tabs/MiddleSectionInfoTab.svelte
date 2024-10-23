<script lang="ts">
    export let testId: string = "";
    async function loadTestInfo() {}
</script>

{#await loadTestInfo()}
    <div class="loading-message-div">Loading test data...</div>
{:then}
    <p>info tab</p>
{/await}
