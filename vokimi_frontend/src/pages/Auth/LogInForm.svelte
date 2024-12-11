<script lang="ts">
    import { Link } from "svelte-routing";
    import InputWithIcon from "./InputWithIcon.svelte";
    import UpdatePasswordDialog from "../../components/shared/dialogs/UpdatePasswordDialog.svelte";

    let email = "";
    let password = "";
    let errorMessage = "";

    async function submitForm() {
        const data = { email, password };
        if (email == "" || password == "") {
            errorMessage = "Please fill in all the fields.";
            return;
        }
        const response = await fetch("/api/login", {
            method: "POST",
            headers: {
                "Content-Type": "application/json",
            },
            body: JSON.stringify(data),
        });

        if (response.ok) {
            window.location.href = "/";
        } else if (response.status === 400) {
            const errorResponse = await response.json();
            errorMessage = errorResponse.error || "An unknown error occurred.";
        } else {
            errorMessage = "An unknown error occurred.";
        }
    }
    let updatePasswordDialog: UpdatePasswordDialog;
</script>

<UpdatePasswordDialog bind:this={updatePasswordDialog} />
<form on:submit|preventDefault={submitForm} class="form-container">
    <p class="form-title">Log in to your account</p>
    <InputWithIcon inputType="email" inputValueName="email" bind:value={email}>
        <svg xmlns="http://www.w3.org/2000/svg" viewBox="0 0 24 24" fill="none">
            <path
                d="M2 5L8.91302 8.92462C11.4387 10.3585 12.5613 10.3585 15.087 8.92462L22 5"
                stroke="currentColor"
                stroke-width="1.5"
                stroke-linejoin="round"
            />
            <path
                d="M10.5 19.5C10.0337 19.4939 9.56682 19.485 9.09883 19.4732C5.95033 19.3941 4.37608 19.3545 3.24496 18.2184C2.11383 17.0823 2.08114 15.5487 2.01577 12.4814C1.99475 11.4951 1.99474 10.5147 2.01576 9.52843C2.08114 6.46113 2.11382 4.92748 3.24495 3.79139C4.37608 2.6553 5.95033 2.61573 9.09882 2.53658C11.0393 2.4878 12.9607 2.48781 14.9012 2.53659C18.0497 2.61574 19.6239 2.65532 20.755 3.79141C21.8862 4.92749 21.9189 6.46114 21.9842 9.52844C21.9939 9.98251 21.9991 10.1965 21.9999 10.5"
                stroke="currentColor"
                stroke-width="1.5"
                stroke-linecap="round"
                stroke-linejoin="round"
            />
            <path
                d="M19 17C19 17.8284 18.3284 18.5 17.5 18.5C16.6716 18.5 16 17.8284 16 17C16 16.1716 16.6716 15.5 17.5 15.5C18.3284 15.5 19 16.1716 19 17ZM19 17V17.5C19 18.3284 19.6716 19 20.5 19C21.3284 19 22 18.3284 22 17.5V17C22 14.5147 19.9853 12.5 17.5 12.5C15.0147 12.5 13 14.5147 13 17C13 19.4853 15.0147 21.5 17.5 21.5"
                stroke="currentColor"
                stroke-width="1.5"
                stroke-linecap="round"
                stroke-linejoin="round"
            />
        </svg>
    </InputWithIcon>
    <InputWithIcon
        inputType="password"
        inputValueName="password"
        bind:value={password}
    >
        <svg xmlns="http://www.w3.org/2000/svg" viewBox="0 0 24 24" fill="none">
            <path
                d="M18 10.9971C17.6078 10.1968 16.5481 9.02153 14.3703 9.07154C14.3703 9.07154 12.6431 8.99652 10.6906 8.99652C8.73815 8.99652 7.82408 9.04224 6.25999 9.07154C5.25872 9.04653 3.35629 9.2716 2.48018 11.3472C1.90445 13.0976 1.87941 16.7736 2.22986 18.6241C2.30496 19.5744 2.80559 20.8998 4.35757 21.6C5.30878 22.1001 6.83573 21.9 7.9872 22.0001M5.98465 8.1963C5.93458 5.82065 5.83445 3.94514 8.58796 2.39472C9.51414 2.01962 10.8909 1.69453 12.5931 2.49475C14.3703 3.57004 14.5917 4.70802 14.7458 4.99543C15.1713 6.12074 14.9461 7.72117 14.9961 8.37135"
                stroke="currentColor"
                stroke-width="1.5"
                stroke-linecap="round"
            />
            <path
                d="M15.5 19.7351C15.5 20.9792 14.4911 21.9656 13.2552 21.9656C12.0194 21.9656 11 20.9792 11 19.7351C11 18.4911 12.0194 17.4915 13.2552 17.4915C14.4911 17.4915 15.5 18.4911 15.5 19.7351Z"
                stroke="currentColor"
                stroke-width="1.5"
            />
            <path
                d="M15.2251 17.7909L17.2156 15.8482M22.0001 15.8482L20.3731 14.3089C19.6001 13.5692 18.9501 14.2149 18.6264 14.4906L17.2156 15.8482M17.2156 15.8482L18.8251 17.3936"
                stroke="currentColor"
                stroke-width="1.5"
                stroke-linecap="round"
            />
        </svg>
    </InputWithIcon>

    <p class="error-message">{errorMessage}</p>
    <button class="login-button" type="submit">Log In</button>
    <div class="links-container">
        <Link to="/auth/signup">
            <span>I don't have an account</span>
        </Link>
        <span on:click={() => updatePasswordDialog.open()}>
            I forgot my password
        </span>
    </div>
</form>

<style>
    .form-container {
        width: 300px;
        display: flex;
        flex-direction: column;
        align-items: center;
        justify-content: center;
        padding: 20px 40px 16px 40px;
        background-color: var(--back-main);
        border-radius: 11px;
        box-shadow: rgba(0, 0, 0, 0.16) 0px 1px 4px;
    }
    .form-title {
        font-size: 28px;
        font-weight: 600;
        margin: 48px 0;
    }
    .error-message {
        margin-top: 8px;
        margin-bottom: 16px;
    }
    .login-button {
        width: 100%;
        height: 40px;
        border: 0;
        background: var(--primary);
        border-radius: 7px;
        outline: none;
        color: var(--back-main);
        cursor: pointer;
    }
    .login-button:hover {
        background: var(--primary-hov);
    }
    .links-container {
        display: flex;
        align-items: center;
        flex-direction: column;
        margin-top: 8px;
    }
    .links-container span {
        color: var(--text-faded);
        text-decoration: underline;
        cursor: pointer;
        font-size: 14px;
        padding: 4px 8px;
        border-radius: 4px;
    }
    .links-container span:hover {
        color: var(--primary);
    }
    .links-container span:active {
        color: var(--primary-hov);
        background-color: var(--back-secondary);
    }
</style>
