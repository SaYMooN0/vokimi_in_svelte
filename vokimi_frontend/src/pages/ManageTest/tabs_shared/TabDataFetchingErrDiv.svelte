<script lang="ts">
    export let err: string;
    export let tryAgainAction: () => void;
</script>

<div class="tab-data-fetching-err-div">
    <p>{err}</p>
    <button on:click={tryAgainAction}>Try again</button>
</div>
