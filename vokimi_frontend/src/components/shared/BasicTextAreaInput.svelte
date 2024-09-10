<script lang="ts">
    export let text: string;
    export let placeholder: string = "";
    export let id: string = "";
</script>

<textarea class="basic-text-area" {id} {placeholder} bind:value={text} />

<style>
    .basic-text-area {
        outline: none;
        border-radius: 8px;
        background-color: var(--back-secondary);
        border: 2px solid transparent;
        padding: 3px 6px;
        box-sizing: border-box;
        font-size: 18px;
    }
    .basic-text-area::placeholder {
        color: var(--text-faded);
        opacity: 1;
    }
    .basic-text-area::-ms-input-placeholder {
        color: var(--text-faded);
    }
    .basic-text-area:focus {
        border-color: var(--primary);
    }
</style>
