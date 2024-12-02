<script lang="ts">
    export let onSaveClicked: () => void;
</script>

<button on:click={onSaveClicked} class="save-btn unselectable">Save</button>

<style>
    .save-btn {
        margin-top: 32px;
        padding: 4px 24px;
        font-size: 22px;
        color: var(--back-main);
        background-color: var(--primary);
        border-radius: 4px;
        outline: none;
        border: none;
        cursor: pointer;
        transition: all 0.08s ease-in;
    }
    .save-btn:hover {
        background-color: var(--primary-hov);
    }
    .save-btn:active {
        transform: scale(0.94);
    }
</style>
