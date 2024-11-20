<script lang="ts">
    export let testId: string;
    export let openCollectionCreationDialog: () => void;
</script>

<div class="no-collections">
    <p>You don't have any collections</p>
    <button class="new-collection-btn" on:click={openCollectionCreationDialog}>
        Create first collection
    </button>
</div>

<style>
    .no-collections {
        width: 100%;
        padding: 0;
        display: flex;
        flex-direction: column;
        align-items: center;
        gap: 4px;
    }
    .no-collections p {
        margin: 4px 0;
        color: var(--text-faded);
        font-size: 18px;
        font-weight: 500;
    }

    .new-collection-btn {
        margin-bottom: 4px;
        padding: 4px 20px;
        border: none;
        outline: none;
        border-radius: 50px;
        background-color: var(--primary);
        color: var(--back-main);
        font-size: 16px;
        cursor: pointer;
    }
    .new-collection-btn:hover {
        background-color: var(--primary-hov);
    }
    .new-collection-btn:active {
        transform: scale(0.98);
    }
</style>
