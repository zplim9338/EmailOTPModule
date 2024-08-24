# Assumptions

## 1. Email Domain Restriction
- **Description**: User email addresses are expected to end with a specific domain provided during initialization.
- **Rationale**: The system assumes that user emails follow a certain domain pattern for verification and security purposes.
- **Impact**: If the user's email address doesn't match the specified domain, the email OTP generation will fail.
- **Mitigation**: Clearly communicate the domain requirement to users during registration or input.

## 2. Validity Duration of OTP
- **Description**: The generated OTP is considered valid for 1 minute.
- **Rationale**: A short validity period is assumed to enhance security by minimizing the window for potential misuse.
- **Impact**: If users fail to input the OTP within the specified timeframe, the verification process will fail, requiring a new OTP generation.
- **Mitigation**: Clearly communicate the expiration timeframe to users.

## 3. OTP Input Attempts Limit
- **Description**: Users have a limited number of attempts to input the OTP correctly before failing.
- **Rationale**: Limiting attempts is assumed to prevent brute-force attacks and unauthorized access attempts.
- **Impact**: After reaching the maximum attempts limit, the user will be prevented from further OTP input, potentially causing inconvenience.
- **Mitigation**: Provide clear feedback to users on the remaining attempts.

## 4. Timeout for User Input
- **Description**: Users are expected to input the OTP within a specific timeframe.
- **Rationale**: A timeout is assumed to prevent indefinite waiting and to ensure the security of the OTP validation process.
- **Impact**: If the user fails to input the OTP within the timeout period, the verification process will terminate.
- **Mitigation**: Clearly communicate the timeout duration to users.

## 5. Security of OTP Generation
- **Description**: An OTP is a 6-digit number generated randomly.
- **Rationale**: A 6-digit numeric OTP is a common practice for simplicity and ease of use.
- **Impact**: There is a 1 in 1,000,000 chance of guessing the OTP by random input.
- **Mitigation**: Increase the complexity of the OTP if necessary.
