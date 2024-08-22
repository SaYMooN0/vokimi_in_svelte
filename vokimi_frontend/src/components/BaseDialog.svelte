<script lang="ts">
    export let dialogId: string;
    let dialogElement;

    export function open() {
        dialogElement.showModal();
    }

    export function close() {
        dialogElement.close();
    }
</script>

<dialog bind:this={dialogElement} id={dialogId} class="base-dialog">
    <slot></slot>
</dialog>

<style>
    .base-dialog {
        outline: none;
        border: none;
        border-radius: 10px;
        padding: 0;
        max-height: unset;
        max-width: unset;
    }
</style>
