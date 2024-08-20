<script>
    export let inputType = "text";
    export let value = "";
    export let inputValueName;

    let placeholder = "Enter your " + inputValueName;
    let inputId = inputValueName + "-field";
    let labelText =
        inputValueName.charAt(0).toUpperCase() + inputValueName.slice(1);
</script>

<div class="input-container">
    <label class="input-label" for={inputId}>{labelText}</label>
    {#if inputType === "email"}
        <input
            type="email"
            {placeholder}
            bind:value
            class="input-field"
            id={inputId}
        />
    {:else if inputType === "password"}
        <input
            type="password"
            {placeholder}
            bind:value
            class="input-field"
            id={inputId}
        />
    {:else}
        <input
            type="text"
            {placeholder}
            bind:value
            class="input-field"
            id={inputId}
        />
    {/if}
    <slot />
</div>

<style>
    .input-container {
        width: 100%;
        height: fit-content;
        position: relative;
        display: flex;
        flex-direction: column;
        gap: 3px;
        margin:8px 0;
    }

    .input-container > :global(svg) {
        height: 24px;
        position: absolute;
        z-index: 99;
        left: 10px;
        bottom: 11px;
        color: var(--text-faded);
        stroke-width: 12;
    }
    .input-container :global(path) {
        stroke-width: 1.8;
    }
    .input-label {
        padding-left: 8px;
        font-size: 14px;
        color: var(--text-faded);
        font-weight: 600;
    }

    .input-field {
        height: 46px;
        padding: 0 0 0 42px;
        border-radius: 7px;
        outline: none;
        color: var(--text);
        font-size: 19px;
        border: 2px solid var(--back-secondary);
        transition: all 0.2s ease-in
    }
    .input-field::placeholder {
        color: var(--text-faded);
    }

    .input-field::-ms-input-placeholder {
        color: var(--text-faded);
    }
    .input-field:focus {
        border-color: var(--primary);
    }
</style>
