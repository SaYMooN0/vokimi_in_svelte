<script lang="ts">
    import type { Err } from "../../../../ts/Err";

    export let fetchTestsFunc: (skipLengthCheck: boolean) => Promise<Err>;
    export let yourTestsLabel: string = "";
</script>

{#await fetchTestsFunc(false)}
    <div class="loading-div">
        <h2>Receiving information from server...</h2>
        <p>Please wait</p>
    </div>
{:then err}
    {#if err.notNone()}
        <div class="error-div">
            <p class="error">{err.toString()}</p>
        </div>
    {:else}
        <h2 class="your-draft-tests-label">{yourTestsLabel}</h2>
        <label class="refresh-label" on:click={() => fetchTestsFunc(true)}>
            Refresh
        </label>
        <slot />
    {/if}
{/await}

<style>
    .your-draft-tests-label {
        margin: 20px auto 0 auto;
        width: fit-content;
    }
    .refresh-label{
        cursor: pointer;
        color: var(--primary);
        margin: 0 auto 20px auto;
        width: fit-content;
    }
</style>
