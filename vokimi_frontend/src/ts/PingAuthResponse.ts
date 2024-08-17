export class PingAuthResponse {
    constructor(
        public readonly Email?: string,
        public readonly Username?: string,
        public readonly UserId?: string
    ) { }

    public isAuthenticated(): boolean {
        return this.Username !== undefined &&
            this.Username !== "" &&
            this.UserId !== undefined &&
            this.UserId !== "" &&
            this.Email !== undefined &&
            this.Email !== "";
    }
}