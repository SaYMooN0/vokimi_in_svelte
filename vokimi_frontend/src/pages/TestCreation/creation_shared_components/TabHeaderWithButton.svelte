<script lang="ts">
    export let tabName: string;
    export let buttonText: string;
    export let onButtonClick: () => void;
</script>

<div class="tab-header">
    <span class="tab-name">{tabName}</span>
    <button class="header-button" on:click={onButtonClick}>{buttonText}</button>
</div>

<style>
    .tab-header {
        display: flex;
        flex-direction: row;
        align-items: center;
        gap: 32px;
        margin-left: 24px;
    }

    .tab-name {
        font-size: 28px;
        font-weight: 500;
    }

    .header-button {
        padding: 8px 24px;
        background-color: var(--primary);
        color: var(--back-main);
        border: 0;
        border-radius: 8px;
        cursor: pointer;
        transition: all 0.1s;
    }
    .header-button:hover {
        background-color: var(--primary-hov);
    }
</style>
