<script lang="ts">
    export let cards: { label: string; value: string }[] = [];
    function openPostCreationDialog() {}
</script>

<div class="cards-container">
    {#each cards as card}
        <div class="card">
            <span class="card-label">{card.label}</span>
            <span class="card-value">{card.value}</span>
            <button
                class="make-post-button unselectable"
                on:click={openPostCreationDialog}
            >
                Make a post about it
            </button>
        </div>
    {/each}
</div>

<style>
    .cards-container {
        width: 100%;
        gap: 4%;
        padding: 0 4%;
        display: flex;
        flex-direction: row;
        flex-wrap: wrap;
        justify-content: center;
        box-sizing: border-box;
    }
    .card {
        margin-top: 20px;
        display: flex;
        flex-direction: column;
        align-items: center;
        background-color: var(--back-secondary);
        box-shadow: 2px 2px 4px rgba(0, 0, 0, 0.08);
        border-radius: 8px;
        width: 260px;
        min-height: 120px;
        padding: 8px 12px;
        box-sizing: border-box;
        position: relative;
    }
    .card-label {
        font-size: 20px;
        color: var(--primary);
        text-align: center;
        margin: auto 0;
        display: block;
        height: fit-content;
    }
    .card-value {
        font-size: 38px;
        font-weight: 600;
        color: var(--primary);
        text-align: center;
    }
    .card:hover .make-post-button {
        opacity: 1 !important;
    }
    .make-post-button {
        position: absolute;
        opacity: 0;
        bottom: 0;
        left: 50%;
        transform: translateX(-50%) translateY(50%);
        width: 180px;
        height: 24px;
        border-radius: 100px;
        border: none;
        background-color: var(--primary);
        color: var(--back-main);
        font-size: 16px;
        cursor: pointer;
        transition: all 0.08s ease-in;
    }
    .make-post-button:hover {
        background-color: var(--primary-hov);
        width: 192px;
    }
</style>
