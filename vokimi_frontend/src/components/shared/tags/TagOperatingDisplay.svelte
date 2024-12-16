<script lang="ts">
    export let tag: string;
    export let isTagAdded: boolean;
    export let isTagAddingState: boolean;
    export let btnOnClick: () => void;
</script>

<div class="tag-display">
    <span>#{tag}</span>
    {#if isTagAddingState && isTagAdded}
        <svg viewBox="0 0 24 24" fill="none">
            <path
                d="M5 14.5C5 14.5 6.5 14.5 8.5 18C8.5 18 14.0588 8.83333 19 7"
                stroke="currentColor"
                stroke-width="1.5"
                stroke-linecap="round"
                stroke-linejoin="round"
            />
        </svg>
    {:else if isTagAddingState && !isTagAdded}
        <svg
            class="add-btn"
            on:click={() => btnOnClick()}
            viewBox="0 0 24 24"
            fill="none"
        >
            <path
                d="M12 4V20M20 12H4"
                stroke="currentColor"
                stroke-width="1.5"
                stroke-linecap="round"
                stroke-linejoin="round"
            />
        </svg>
    {:else}
        <svg
            class="remove-btn"
            on:click={() => btnOnClick()}
            viewBox="0 0 24 24"
            fill="none"
        >
            <path
                d="M20 12L4 12"
                stroke="currentColor"
                stroke-width="1.5"
                stroke-linecap="round"
                stroke-linejoin="round"
            />
        </svg>
    {/if}
</div>

<style>
    .tag-display {
        min-width: 320px;
        width: 540px;
        margin: 6px 2px;
        padding: 4px 0;
        box-sizing: border-box;
        display: grid;
        grid-template-columns: 1fr auto;
        border-radius: 4px;
        box-shadow:
            rgba(0, 0, 0, 0.02) 0px 1px 3px 0px,
            rgba(27, 31, 35, 0.15) 0px 0px 0px 1px;
    }

    .tag-display span {
        margin: auto 6px;
        font-size: 20px;
            word-break: break-all;
    }

    .tag-display svg {
        margin: auto 6px;
        height: 24px;
        aspect-ratio: 1/1;
        box-sizing: border-box;
        padding: 4px;
        color: var(--back-main);
        background-color: var(--text-faded);
        border-radius: 16%;
        cursor: pointer;
        transition: all 0.08s ease-in;
    }

    .tag-display svg path {
        stroke-width: 2.6;
    }

    .add-btn:hover {
        background-color: var(--primary);
    }

    .remove-btn:hover {
        background-color: var(--red-del);
    }
</style>
