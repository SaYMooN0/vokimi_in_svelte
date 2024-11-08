<script lang="ts">
    export let isChecked: boolean = false;
    export let id: string = "";
</script>

<label class="switch">
    <input type="checkbox" {id} bind:checked={isChecked} />
    <span class="slider"></span>
</label>

<style>
    .switch {
        --button-width: 2.8em;
        --button-height: 1.6em;
        --toggle-diameter: 1.1em;
        --button-toggle-offset: calc(
            (var(--button-height) - var(--toggle-diameter)) / 2
        );
        --toggle-shadow-offset: 10px;
        --color-grey: var(--text-faded);
        height: var(--button-height);
    }

    .slider {
        display: inline-block;
        width: var(--button-width);
        height: var(--button-height);
        background-color: var(--color-grey);
        border-radius: calc(var(--button-height) / 2);
        position: relative;
        transition: 0.3s all ease-in-out;
    }

    .slider::after {
        content: "";
        display: inline-block;
        width: var(--toggle-diameter);
        height: var(--toggle-diameter);
        background-color: var(--back-main);
        border-radius: calc(var(--toggle-diameter) / 2);
        position: absolute;
        top: var(--button-toggle-offset);
        transform: translateX(var(--button-toggle-offset));
        box-shadow: var(--toggle-shadow-offset) 0
            calc(var(--toggle-shadow-offset) * 4) rgba(0, 0, 0, 0.1);
        transition: 0.3s all ease-in-out;
    }

    .switch input[type="checkbox"]:checked + .slider {
        background-color: var(--primary);
    }

    .switch input[type="checkbox"]:checked + .slider::after {
        transform: translateX(
            calc(
                var(--button-width) - var(--toggle-diameter) -
                    var(--button-toggle-offset)
            )
        );
        box-shadow: calc(var(--toggle-shadow-offset) * -1) 0
            calc(var(--toggle-shadow-offset) * 4) rgba(0, 0, 0, 0.1);
    }

    .switch input[type="checkbox"] {
        display: none;
    }
</style>
