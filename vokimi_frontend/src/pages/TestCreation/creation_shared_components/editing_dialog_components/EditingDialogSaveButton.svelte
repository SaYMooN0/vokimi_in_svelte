<script lang="ts">
    export let onClick: () => void;
</script>

<button class="save-button" on:click={onClick}>Save</button>

<style>
    .save-button {
        padding: 10px 20px;
        background-color: var(--primary);
        color: var(--back-main);
        border: 0;
        border-radius: 8px;
    }
</style>
