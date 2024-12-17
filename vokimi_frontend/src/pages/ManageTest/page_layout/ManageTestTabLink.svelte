<script lang="ts">
    export let text: string;
    export let onClick: () => void;
    export let isActive: boolean;
</script>

<div
    class="manage-test-tab-link unselectable"
    on:click={onClick}
    class:active={isActive}
>
    <slot />
    <span>
        {text}
    </span>
</div>

<style>
    .manage-test-tab-link {
        display: flex;
        flex-direction: row;
        align-items: center;
        color: var(--text-faded);
        gap: 4px;
        cursor: pointer;
        padding: 2px 16px;
        border-radius: 32px;
        border: 2px solid transparent;
        background-color: var(--back-secondary);
    }
    .manage-test-tab-link.active {
        border-color: var(--primary);
    }
    .manage-test-tab-link span {
        color: inherit;
        font-size: 18px;
        font-weight: 600;
    }
    .manage-test-tab-link :global(.manage-test-tab-icon) {
        color: inherit;
        display: flex;
        height: 32px;
        width: 32px;
    }
    .manage-test-tab-link:hover {
        color: var(--primary);
    }
</style>
