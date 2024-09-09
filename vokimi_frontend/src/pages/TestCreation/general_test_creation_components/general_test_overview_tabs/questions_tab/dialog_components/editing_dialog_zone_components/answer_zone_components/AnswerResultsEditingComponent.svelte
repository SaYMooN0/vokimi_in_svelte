<script lang="ts">
    export let relatedResults: { [key: string]: string };
    export let openResultAssigningDialog: () => void;

    function removeResult(key: string) {
        const { [key]: omitted, ...rest } = relatedResults;
        relatedResults = rest;
    }
</script>

<div class="related-results-container">
    {#each Object.keys(relatedResults) as resKey}
        <div class="chosen-result">
            <label class="result-label">
                {relatedResults[resKey]}
            </label>
            <div class="result-label-tooltip">
                {relatedResults[resKey]}
            </div>
            <svg
                class="remove-result-btn"
                on:click={() => removeResult(resKey)}
                viewBox="0 0 448 512"
                xmlns="http://www.w3.org/2000/svg"
                xmlns:xlink="http://www.w3.org/1999/xlink"
            >
                <path
                    d="M432 256c0 17.7-14.3 32-32 32L48 288c-17.7 0-32-14.3-32-32s14.3-32 32-32l352 0c17.7 0 32 14.3 32 32z"
                />
            </svg>
        </div>
    {/each}
    {#if Object.keys(relatedResults).length < 5}
        <div class="add-new-result-btn" on:click={openResultAssigningDialog}>
            <svg
                viewBox="0 0 448 512"
                xmlns="http://www.w3.org/2000/svg"
                xmlns:xlink="http://www.w3.org/1999/xlink"
            >
                <path
                    d="M256 80c0-17.7-14.3-32-32-32s-32 14.3-32 32V224H48c-17.7 0-32 14.3-32 32s14.3 32 32 32H192V432c0 17.7 14.3 32 32 32s32-14.3 32-32V288H400c17.7 0 32-14.3 32-32s-14.3-32-32-32H256V80z"
                />
            </svg>
            related result
        </div>
    {/if}
</div>

<style>
    .related-results-container {
        margin: 16px 0;
        display: flex;
        flex-direction: column;
        justify-content: center;
        align-items: center;
        gap: 4px;
        width: 180px;
    }
    .add-new-result-btn {
        width: 92%;
        background-color: var(--primary);
        display: flex;
        justify-content: center;
        align-items: center;
        gap: 4%;
        padding: 4px 0;
        border-radius: 4px;
        color: var(--back-main);
        font-size: 18px;
        cursor: pointer;
        transition: all 0.12s ease-in;
    }

    .add-new-result-btn svg {
        height: 18px;
        fill: var(--back-main);
    }
    .add-new-result-btn:hover {
        background-color: var(--primary-hov);
        gap: 8%;
        width: 98%;
    }

    .add-new-result-btn:active {
        transform: scale(0.97);
    }
</style>
