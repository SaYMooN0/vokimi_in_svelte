<script lang="ts">
    export let onClick: () => void;
    export let buttonText: string = "Save";
</script>

<button class="save-button unselectable" on:click={onClick}>
    {buttonText}
</button>

<style>
    .save-button {
        justify-self: center;
        align-self: center;
        width: fit-content;
        padding: 8px 32px;
        background-color: var(--primary);
        color: var(--back-main);
        border: 0;
        border-radius: 8px;
        font-size: 22px;
        cursor: pointer;
        transition: all 0.12s ease;
    }

    .save-button:hover {
        background-color: var(--primary-hov);
    }
</style>
