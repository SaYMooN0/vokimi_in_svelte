<script lang="ts">
    export let actionOnClick: () => void;
    export let actionText: string;
    export let actionColor: string;
</script>

<div
    class="action-item"
    style="--action-color: {actionColor}"
    on:click={actionOnClick}
>
    <slot />
    <span>
        {actionText}
    </span>
</div>

<style>
    .action-item {
        display: grid;
        height: 48px;
        width: 240px;
        padding: 4px 8px;
        box-sizing: border-box;
        grid-template-columns: auto 1fr;
        gap: 4px;
        border: 2px solid var(--action-color);
        border-radius: 8px;
        align-items: center;
        cursor: pointer;
        background-color: var(--back-secondary);
        transition: all 0.08s ease-in;
    }
    .action-item span {
        color: var(--action-color);
        font-size: 20px;
        font-weight: 500;
    }
    .action-item:hover {
        transform: scale(1.04);
        gap: 8px;
    }

    .action-item :global(svg) {
        height: 32px;
        aspect-ratio: 1/1;
        color: var(--action-color);
    }
</style>
