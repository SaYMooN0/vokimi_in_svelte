<script lang="ts">
    export let err: string;
    export let tryAgainAction: () => void;
</script>

<div class="tab-data-fetching-err-div">
    <p>{err}</p>
    <button on:click={tryAgainAction}>Try again</button>
</div>

<style>
    .tab-data-fetching-err-div {
        display: flex;
        flex-direction: column;
        align-items: center;
        margin-top: 16vh;
    }
    p {
        font-size: 32px;
        margin:8px;
    }
</style>
